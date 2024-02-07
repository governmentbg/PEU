using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models
{
    public class PersonAndEntityBasicDataVM 
    {
        #region Private Member

        private PersonBasicDataVM _itemPersonBasicData;
        private ForeignCitizenBasicData _itemForeignCitizenBasicData;
        private EntityBasicData _itemEntityBasicData;
        private ForeignEntityBasicDataVM _itemForeignEntityBasicData;

        #endregion
        
        public enum PersonAndEntityChoiceType
        {
            /// <summary>
            /// Person.
            /// </summary>
            Person,

            /// <summary>
            /// ForeignCitizen.
            /// </summary>
            ForeignPerson,

            /// <summary>
            /// Entity.
            /// </summary>
            Entity,

            /// <summary>
            /// ForeignEntity.
            /// </summary>
            ForeignEntity
        }

        #region Porperties
        
        public PersonBasicDataVM ItemPersonBasicData
        {
            get
            {
                return _itemPersonBasicData;
            }
            set
            {
                _itemPersonBasicData = value;
                if (value != null)
                    SelectedChoiceType = PersonAndEntityChoiceType.Person;
            }
        }


        public ForeignCitizenBasicData ItemForeignCitizenBasicData
        {
            get
            {
                return _itemForeignCitizenBasicData;
            }
            set
            {
                _itemForeignCitizenBasicData = value;
                if (value != null)
                    SelectedChoiceType = PersonAndEntityChoiceType.ForeignPerson;
            }
        }

        public EntityBasicData ItemEntityBasicData
        {
            get
            {
                return _itemEntityBasicData;
            }
            set
            {
                _itemEntityBasicData = value;
                if (value != null)
                    SelectedChoiceType = PersonAndEntityChoiceType.Entity;
            }
        }

        public ForeignEntityBasicDataVM ItemForeignEntityBasicData
        {
            get
            {
                return _itemForeignEntityBasicData;
            }
            set
            {
                _itemForeignEntityBasicData = value;
                if (value != null)
                    SelectedChoiceType = PersonAndEntityChoiceType.ForeignEntity;
            }
        }

        public PersonAndEntityChoiceType SelectedChoiceType
        {
            get;
            set;
        }

        #endregion
    }
}
