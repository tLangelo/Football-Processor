namespace Football_Processor
{
    public enum TextDividerType
    {
        Basic,
        Double,
        Thick,
        Dashed,
        Wavy
    }

    public class UI_Elements
    {
        public UI_Elements() { }

        public void GetDivider(TextDividerType type, string text)
        {
            switch (type)
            {
                case TextDividerType.Basic:
                    Console.WriteLine($"------ {text.ToUpper()} ------");
                    break;
                case TextDividerType.Double:
                    Console.WriteLine($"====== {text.ToUpper()} ======");
                    break;
                case TextDividerType.Thick:
                    Console.WriteLine($"###### {text.ToUpper()} ######");
                    break;
                case TextDividerType.Dashed:
                    Console.WriteLine($"- - - {text.ToUpper()} - - -");
                    break;
                case TextDividerType.Wavy:
                    Console.WriteLine($"~~~~~~ {text.ToUpper()} ~~~~~~");
                    break;
                default:
                    throw new ArgumentException("Invalid divider type", nameof(type));
            }
        }
    }
}
