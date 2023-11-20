using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(ParticleSystem))]
public class PickUoCoinAnimation : MonoBehaviour
{
    
    ParticleSystem par;
    ParticleSystem.Particle[] arrPar;
    RectTransform target;
    int arrCount = 0;
    
    public float speed = 0.1f;

    void Awake()
    {
        par = GetComponent<ParticleSystem>();
        arrPar = new ParticleSystem.Particle[par.main.maxParticles];
    }
    void Update()
    {
        if (target && par && par.isPlaying)
        {
            arrCount = par.GetParticles(arrPar);//获取到当前激活的粒子
            Vector3 pos = Vector3.zero;
            for (var i = 0; i < arrCount; i++)
            {
                arrPar[i].position = Vector3.Lerp(arrPar[i].position, pos, speed);//设置他们的位置
            }
            par.SetParticles(arrPar, arrCount);//再把更新过的粒子数据设置回去
        }
    }
    public void Play()
    {
        if (par.isStopped)
        {
            par.Play(true);//供外边调用
        }
    } 
    public void SetTarget(RectTransform transform)
    {
        target = transform;
        ParticleSystem.MainModule module = par.main;//更改生成的空间，使用自定义的
        module.simulationSpace = ParticleSystemSimulationSpace.Custom;
        module.customSimulationSpace = transform;//把target设置给它
    }

    //新加了一个play，可以根据不同需求去设置并且播放。
    public void Play(RectTransform transform)
    {
        SetTarget(transform);
        Play();
    }

}
