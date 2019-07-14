    using System.Collections;
    using System.Collections.Generic;
    using System.Text; 
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using System.Linq; 

    [System.Serializable]


    public class Slot
    {

        public Slot(char c, int i)
        {
            character = c; 
            /// index of the character in the displayed string
            index = i;


            position = Vector3.positiveInfinity; 
        }
    
        /// <summary>
        /// Placeholder slot. Not to be used. 
        /// </summary>
        public Slot()
        {
            character = '\0'; 
            index = -1;
            position = Vector3.positiveInfinity;
        }

        public char character;
        public int index;
        /// <summary>
        /// Position of the character in world-space. Positive infinity if no character exists.
        /// </summary>
        public Vector3 position; 
    }

    [RequireComponent(typeof(Text))]
    public class HiddenPhrase : MonoBehaviour
    {
        public string completeText;
        public Text textComp;
        public List<Slot> openSlots; 

    
        private void Awake()
        {
            GetComponentInChildren<Text>().text = completeText;
            InitializeOpenSlots(); 
        }

        private void Update()
        {
            updateOSPos(); 
        }

        void InitializeOpenSlots()
        {
            openSlots = new List<Slot>(completeText.Replace(" ", "").Length);
            for (int i = 0; i < completeText.Length; i++)
            {
                if (completeText[i] != ' ')
                {
                    openSlots.Add(new Slot(completeText[i], i)); 
                }
            }
        }

        /// <summary>
        /// Updates the position field of all the slots in openSlots. 
        /// </summary>
        void updateOSPos()
        {
            foreach(Slot s in openSlots)
            {
                s.position = GameManager.Instance.activePhrase.GetComponent<CharPosition>().GetWorldPosition(s.index);
            }
        }

        /// <summary>
        /// Removes the slot at the specified index when a character has filled that slot.
        /// Adds a new phrase if there are no slots remaining. 
        /// </summary>
        /// <param name="index"></param>
        public void closeSlot(int index)
        {
            for(int i = 0; i < openSlots.Count; i++)
            {
                if (openSlots[i].index == index)
                {
                    openSlots.RemoveAt(i);
                    if (openSlots.Count == 0)
                        GameManager.Instance.NextPhrase(); 
                }
            }
        }

        /// <summary>
        /// Clears the list of open slots. 
        /// </summary>
        public void CloseAllOpenSlots()
        {
            openSlots = new List<Slot>(); 
        }
    }
