namespace Zettelkasten.DesktopApp.ViewModels
{
    public class ZettelNoteNew : ViewModelBase
    {
        public ZettelNoteNew()
        {
            Init();
        }

        private void Init()
        {
            Name = "новая идея...";
            CreatedAt = DateTime.Now;
            Tag = "";
            Content = "заполни меня...";
        }

        public void Clear()
        {
            Init();
        }

        public bool IsValid()
        {
            return
                (!string.IsNullOrWhiteSpace(Name))
                && CreatedAt >= DateTime.Now.Date;
        }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
    }
}
