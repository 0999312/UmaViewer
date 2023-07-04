using CriWare;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UmaViewerAudio
{
    static public void LoadLiveSoundCri(int songid, UmaDatabaseEntry SongAwb)
    {
        Debug.Log(SongAwb.Name);

        //��ȡ������
        Debug.Log(CriAtomExAcf.GetNumDspSettings());

        string busName = CriAtomExAcf.GetDspSettingNameByIndex(0);

        var dspSetInfo = new CriAtomExAcf.AcfDspSettingInfo();



        Debug.Log(CriAtomExAcf.GetDspSettingInformation(busName, out dspSetInfo));

        Debug.Log(dspSetInfo.name);
        Debug.Log(dspSetInfo.numExtendBuses);
        Debug.Log(dspSetInfo.numBuses);
        Debug.Log(dspSetInfo.numSnapshots);
        Debug.Log(dspSetInfo.snapshotStartIndex);

        var busInfo = new CriAtomExAcf.AcfDspBusInfo();

        for (ushort i = 0; i < dspSetInfo.numExtendBuses; i++)
        {
            Debug.Log(i);
            Debug.Log(CriAtomExAcf.GetDspBusInformation(i, out busInfo));

            Debug.Log(busInfo.name);
            Debug.Log(busInfo.volume);
            Debug.Log(busInfo.numFxes);
            Debug.Log(busInfo.numBusLinks);
        }

        //��ȡAcb�ļ���Awb�ļ���·��
        string nameVar = SongAwb.Name.Split('.')[0].Split('/').Last();

        //ʹ��Live��Bgm
        //nameVar = $"snd_bgm_live_{songid}_oke";

        LoadSound Loader = (LoadSound)ScriptableObject.CreateInstance("LoadSound");
        LoadSound.UmaSoundInfo soundInfo = Loader.getSoundPath(nameVar);

        //��Ƶ������·����������Ƶ
        CriAtom.AddCueSheet(nameVar, soundInfo.acbPath, soundInfo.awbPath);

        //��õ�ǰ��Ƶ��Ϣ
        CriAtomEx.CueInfo[] cueInfoList;
        List<string> cueNameList = new List<string>();
        cueInfoList = CriAtom.GetAcb(nameVar).GetCueInfoList();
        foreach (CriAtomEx.CueInfo cueInfo in cueInfoList)
        {
            cueNameList.Add(cueInfo.name);
            Debug.Log(cueInfo.type);
            Debug.Log(cueInfo.userData);
        }

        //����������
        CriAtomSource source = new GameObject("CuteAudioSource").AddComponent<CriAtomSource>();
        source.transform.SetParent(GameObject.Find("AudioManager/AudioControllerBgm").transform);
        source.cueSheet = nameVar;

        //����
        source.Play(cueNameList[0]);

        //source.SetBusSendLevelOffset(1, 1);


        /*
        for (int i = 0; i < 17; i++)
        {
            Debug.Log(source.player.GetParameterFloat32((CriAtomEx.Parameter)i));
        }
        */

    }
}
