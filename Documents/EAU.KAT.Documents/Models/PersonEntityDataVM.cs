namespace EAU.KAT.Documents.Models
{
    public class PersonEntityDataVM
    {
        public PersonEntityChoiceType SelectedChoiceType { get; set; }

        public PersonEntityFarmerChoiceType SelectedPersonEntityFarmerChoiceType { get; set; }

        public PersonDataVM Person { get; set; }

        public EntityDataVM Entity { get; set; }

        public bool? IsFarmer { get; set; }
    }
}