using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu]
 public class DatosNivelJuego : ScriptableObject
    {
        [System.Serializable]
        public struct CategoryRecord
        {
            public string categoryName;
            public List<DatosTablero> boardData;

        }
        public List<CategoryRecord> data;
 }

