using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class HighLightning
    {
        int _textLimitAutoHighLight = 300;
        bool _isVolumeRichTextBox = false;
        bool _isHighLight = false;

        public int TextLimitAutoHighLight { get => _textLimitAutoHighLight; }
        public bool IsVolumeRichTextBox { get => _isVolumeRichTextBox; set => _isVolumeRichTextBox = value; }
        public bool IsHighLight { get => _isHighLight; set => _isHighLight = value; }


        // Подсветка синтаксиса
        public void HighlightKeywords(RichTextBox _currentRichTextBox, TextManager text_manager)
        {
            int selectionStart = _currentRichTextBox.SelectionStart;
            int selectionLength = _currentRichTextBox.SelectionLength;
            _isHighLight = true;
            _currentRichTextBox.SuspendLayout();

            try
            {
                _currentRichTextBox.SelectionStart = 0;
                _currentRichTextBox.SelectionLength = _currentRichTextBox.TextLength;
                _currentRichTextBox.SelectionColor = text_manager.SelectedColor;

                // Подсветка комментариев
                HighlightComments(_currentRichTextBox, text_manager.KeywordCategories["Comments"]);

                foreach (var category in text_manager.Keywords)
                {
                    string categoryName = category.Key;
                    List<string> keywordList = category.Value;
                    Color keywordColor = text_manager.KeywordCategories[categoryName];

                    foreach (string keyword in keywordList)
                    {
                        int index = 0;
                        while (index < _currentRichTextBox.Text.Length)
                        {
                            index = _currentRichTextBox.Text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase);
                            if (index >= 0)
                            {
                                if ((index == 0 || !char.IsLetterOrDigit(_currentRichTextBox.Text[index - 1])) &&
                                    (index + keyword.Length == _currentRichTextBox.Text.Length || !char.IsLetterOrDigit(_currentRichTextBox.Text[index + keyword.Length])))
                                {
                                    _currentRichTextBox.SelectionStart = index;
                                    _currentRichTextBox.SelectionLength = keyword.Length;
                                    _currentRichTextBox.SelectionColor = keywordColor;
                                }
                                index += keyword.Length;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                _currentRichTextBox.SelectionStart = selectionStart;
                _currentRichTextBox.SelectionLength = selectionLength;
                _currentRichTextBox.SelectionColor = text_manager.SelectedColor;
                _currentRichTextBox.ResumeLayout();
                _currentRichTextBox.Focus();
            }
        }

        // Подсветка комментов
        private void HighlightComments(RichTextBox richTextBox, Color commentColor)
        {
            string text = richTextBox.Text;
            int start = 0;
            while ((start = text.IndexOf("//", start)) >= 0)
            {
                int end = text.IndexOf("\n", start);
                if (end < 0)
                {
                    end = text.Length;
                }
                int length = end - start;

                richTextBox.SelectionStart = start;
                richTextBox.SelectionLength = length;
                richTextBox.SelectionColor = commentColor;

                start = end;
            }
        }
    }
}
