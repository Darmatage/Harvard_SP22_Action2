
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TypeFX: MonoBehaviour {

        public float delay = 0.01f;
        public string fullText;
        private string currentText = "";

        void Start(){
                StartCoroutine(ShowText());
        }

        IEnumerator ShowText(){
                for(int i = 0; i < (fullText.Length + 1); i++){
                        currentText = fullText.Substring(0,i);
                        this.GetComponent<Text>().text = currentText;
                        yield return new WaitForSeconds(delay);
                }
        }
}