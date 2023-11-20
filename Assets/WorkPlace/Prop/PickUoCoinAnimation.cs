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
            arrCount = par.GetParticles(arrPar);//��ȡ����ǰ���������
            Vector3 pos = Vector3.zero;
            for (var i = 0; i < arrCount; i++)
            {
                arrPar[i].position = Vector3.Lerp(arrPar[i].position, pos, speed);//�������ǵ�λ��
            }
            par.SetParticles(arrPar, arrCount);//�ٰѸ��¹��������������û�ȥ
        }
    }
    public void Play()
    {
        if (par.isStopped)
        {
            par.Play(true);//����ߵ���
        }
    } 
    public void SetTarget(RectTransform transform)
    {
        target = transform;
        ParticleSystem.MainModule module = par.main;//�������ɵĿռ䣬ʹ���Զ����
        module.simulationSpace = ParticleSystemSimulationSpace.Custom;
        module.customSimulationSpace = transform;//��target���ø���
    }

    //�¼���һ��play�����Ը��ݲ�ͬ����ȥ���ò��Ҳ��š�
    public void Play(RectTransform transform)
    {
        SetTarget(transform);
        Play();
    }

}
