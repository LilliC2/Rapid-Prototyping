using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : GameBehaviour<UISkillTree>
{
    [Header("Feedback")]
    public GameObject skillMaxedImage;
    public GameObject noPointsImage;
    public GameObject skillLockedImage;

    public TMP_Text skillGainedText;

    public TMP_Text waveComplete;

    [Header("Skill 1 - Bullet Time")]
    int skill1Modifers = 4;
    public TMP_Text skill1ModifersText;
    bool skill1Unlocked;

    [Header("Skill 2 - Bullet Speed")]
    int skill2Modifers = 6;
    public TMP_Text skill2ModifersText;
    bool skill2Unlocked = false;
    public GameObject skill2LockedImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnlockSkills();

    }

    void UnlockSkills()
    {
        //unlock skill2
        if (skill1Unlocked)
        {
            skill2LockedImage.SetActive(false);
        }
    }

    public void Skill1()
    {
        float skillPoints = _PC2.skillPoints;

        int pointsToUnlock = 4;

        //check if player has appropriate skill points
        if (skillPoints >= pointsToUnlock && skill1Modifers != 0)
        {
            skill1Unlocked = true;
            
            print("Skill 1 aquired!");

            // if yes, upgrade the skill
            _PC2.skillPoints -= pointsToUnlock;
            skill1Modifers--;

            //update ui
            //skill points
            //skill modifers
            skill1ModifersText.text = "Remaing Modifers: " + skill1Modifers + "/4";

            //upgrade to skill
            _PC2.timeBetweenShots -= 0.2f;
            StartCoroutine(SkillGained("Time between shots improved by 0.2 seconds!"));
            print("Time between shots improved by 0.2 seconds!");


        }
        // if no, indicate not enough points
        else if(skillPoints < pointsToUnlock)
        {
            StartCoroutine(NoPoints());
        }
        else if(skill1Modifers == 0)
        {
            StartCoroutine(SkillMaxed());
        }

    }

    public void Skill2()
    {
        float skillPoints = _PC2.skillPoints;

        int pointsToUnlock = 4;

        


        //check if player has appropriate skill points, modifers and if previous skill has been unlocked
        if (skillPoints >= pointsToUnlock && skill1Modifers != 0 && skill1Unlocked)
        {
            skill1Unlocked = true;

            print("Skill 2 aquired!");

            // if yes, upgrade the skill
            _PC2.skillPoints -= pointsToUnlock;
            skill2Modifers--;

            //update ui
            //skill points
            //skill modifers
            skill2ModifersText.text = "Remaing Modifers: " + skill2Modifers + "/6";

            //upgrade to skill
            _PC2.bulletSpeed += 0.5f;
            StartCoroutine(SkillGained("Bullet speed increased by 0.5!"));
            print("Bullet speed increased by 0.5!");


        }
        else if (!skill1Unlocked)
        {
            StartCoroutine(SkillNotUnlocked());
        }
        // if no, indicate not enough points
        else if (skillPoints < pointsToUnlock)
        {
            StartCoroutine(NoPoints());
        }
        else if (skill1Modifers == 0)
        {
            StartCoroutine(SkillMaxed());
        }
        
    }

    IEnumerator SkillGained(string _improvement)
    {
        skillGainedText.text = _improvement;
        skillGainedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        skillGainedText.gameObject.SetActive(false);

    }

    IEnumerator SkillMaxed()
    {
        skillMaxedImage.SetActive(true);
        yield return new WaitForSeconds(2);
        skillMaxedImage.SetActive(false);
    }    
    IEnumerator SkillNotUnlocked()
    {
        skillLockedImage.SetActive(true);
        yield return new WaitForSeconds(2);
        skillLockedImage.SetActive(false);
    }
    IEnumerator NoPoints()
    {
        noPointsImage.SetActive(true);
        yield return new WaitForSeconds(2);
        noPointsImage.SetActive(false);
    }



}
