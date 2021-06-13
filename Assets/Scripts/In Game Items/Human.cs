using UnityEngine;

namespace In_Game_Items
{
    public class Human : Collectible
    {
        private Animator _anim;

        internal override void Awake()
        {
            base.Awake();
            _anim = GetComponent<Animator>();
        }
        public override void DestroyCollectible()
        {
            base.DestroyCollectible();
            _anim.SetBool("he_coming", true);

            SpriteRenderer[] listOfBodyParts = GetComponentsInChildren<SpriteRenderer>();
            StartCoroutine(UsefulFunctions.FadeRenderers(listOfBodyParts, 1.4f));
        }
    }
}