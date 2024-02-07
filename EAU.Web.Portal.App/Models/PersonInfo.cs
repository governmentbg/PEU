namespace EAU.Web.Portal.App.Models
{
    public class CyrilicLatinName
    {
        public string Cyrillic { get; set; }

        public string Latin { get; set; }
    }

    public class PersonInfo
    {
        public string PIN { get; set; }

        public CyrilicLatinName FirstName { get; set; }

        public CyrilicLatinName Surname { get; set; }

        public CyrilicLatinName Family { get; set; }
    }
}