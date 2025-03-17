
namespace Compiler.Model
{
    // Класс для работы с шрифтам, цветом и подсветкой
    public class TextManager
    {
        readonly Dictionary<string, List<string>> _keywords = new Dictionary<string, List<string>>()
    {
        { "Keywords", new List<string> { "public", "private", "class", "void", "new" } },
        { "TypesData", new List<string> { "int", "string", "bool", "float", "double" } },
        { "Operators", new List<string> { "if", "else", "for", "while", "return" } }
    };
        Font _selectedFont = new Font("Verdana", 12F);
        Font _selectedFontOutput = new Font("Verdana", 10F);
        Color _selectedColor = Color.Black;
        Dictionary<string, Color> _keywordCategories = new Dictionary<string, Color>()
    {
        { "Keywords", Color.Blue },    // Основные ключевые слова (синий)
        { "TypesData", Color.Purple },   // Типы данных (зеленый)
        { "Operators", Color.Red },      // Операторы (красный)
        { "Comments", Color.Green }      // Комментарии (серый)
    };

        public Dictionary<string, List<string>> Keywords { get => _keywords; }
        public Font SelectedFont { get => _selectedFont; set => _selectedFont = value; }
        public Font SelectedFontOutput { get => _selectedFontOutput; set => _selectedFontOutput = value; }
        public Color SelectedColor { get => _selectedColor; set => _selectedColor = value; }
        public Dictionary<string, Color> KeywordCategories { get => _keywordCategories; set => _keywordCategories = value; }

        // Выбор шрифта
        public void SettingsFont()
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = SelectedFont;
            
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFont = fontDialog.Font;
            }
        }
        // Выбор шрифта для окна вывода
        public void SettingsFontOutput()
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = SelectedFontOutput;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFontOutput = fontDialog.Font;
            }
        }
        // Выбор цвета
        public Color SettingsColorFont(Color currentColor)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor; if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
            return currentColor;
        }
    }
}
