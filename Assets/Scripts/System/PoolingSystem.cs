using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class PoolingSystem : Singleton<PoolingSystem>
    {
        public ParticleSystem collideEff; //Prefab - sinh ra 10 cai particle
        public ParticleSystem starEff; // Prefab - sinh 10 cai particle
        public ParticleSystem explosionBall; // Prefab - sinh 10 cai particle
        public Ball ball; // Prefab - sinh ra 10 bong

        private List<ParticleSystem> collideEffects; // Pool
        private List<ParticleSystem> getStarEffects; // Pool
        private List<ParticleSystem> explosionBallPool; // Pool
        private List<Ball> ballsPool; // Pool

        protected override void Awake()
        {
            base.Awake();
            collideEffects = new List<ParticleSystem>();
            getStarEffects = new List<ParticleSystem>();
            explosionBallPool = new List<ParticleSystem>();
            ballsPool = new List<Ball>();
        }

        private int indexCollide = 0;
        public ParticleSystem GetCollideEffect()
        {
            if (indexCollide >= collideEffects.Count)
            {
                ParticleSystem par = Instantiate(collideEff, transform);
                par.gameObject.SetActive(false);
                collideEffects.Add(par);
            }
            return collideEffects[indexCollide++];
        }

        private int indexStar = 0;
        public ParticleSystem GetStarEffect()
        {
            if (indexStar >= getStarEffects.Count)
            {
                ParticleSystem par = Instantiate(starEff, transform);
                par.gameObject.SetActive(false);
                getStarEffects.Add(par);
            }
            return getStarEffects[indexStar++];
        }

        private int indexBall = 0;
        public Ball GetBall()
        {
            if (indexBall >= ballsPool.Count)
            {
                Ball par = Instantiate(ball, transform);
                par.gameObject.SetActive(false);
                ballsPool.Add(par);
            }
            ballsPool[indexBall].SetSkin();
            return ballsPool[indexBall++];
        }

        private int indexExplosionBall = 0;
        public ParticleSystem GetExplosion()
        {
            if (indexExplosionBall >= explosionBallPool.Count)
            {
                ParticleSystem par = Instantiate(explosionBall, transform);
                par.gameObject.SetActive(false);
                explosionBallPool.Add(par);
            }
            return explosionBallPool[indexExplosionBall++];
        }

        public void ResetPool()
        {
            indexBall = indexCollide = indexStar = indexExplosionBall = 0;
            foreach (ParticleSystem p in collideEffects)
            {
                p.gameObject.SetActive(false);
            }
            foreach (ParticleSystem p in getStarEffects)
            {
                p.gameObject.SetActive(false);
            }
            foreach (ParticleSystem p in explosionBallPool)
            {
                p.gameObject.SetActive(false);
            }
            foreach (Ball p in ballsPool)
            {
                p.gameObject.SetActive(false);
            }
        }
    }
}