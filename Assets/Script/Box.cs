using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject bomb;

    public bool isArmed { get; private set; }
    public string destination = "test";
    public bool isFragile;
    public bool isBroken;
    public bool isSus;
    public bool isStored;

    List<GameObject> stickers = new List<GameObject>();

    int rnd;

    void Awake()
    {
        if (Random.Range(0, 100) < GameManager.Instance.percentageValid)
        {
            destination = GameManager.Instance.validDestinationLevel[Random.Range(0, GameManager.Instance.validDestinationLevel.Count)];
            GameManager.Instance.valid++;
        }
        else
        {
            destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];
            GameManager.Instance.invalid++;
        }
        rnd = Random.Range(0, 100);
        if (rnd < GameManager.Instance.percentageFragile)
        {
            isFragile = true;
            GameManager.Instance.fragile++;
        }
        else if (rnd < GameManager.Instance.percentageSus)
        {
            isSus = true;
            GameManager.Instance.sus++;
        }
        else
        {
            GameManager.Instance.normal++;
        }

        isArmed = false;
        if(!isFragile)
            if (Random.Range(0, 100) < GameManager.Instance.percentageBomb) 
            {
                bomb.SetActive(true);
                isArmed = true;
                isSus = true;
                destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];
                GameManager.Instance.bomb++;
            }
        isStored = false;

        ApplyStickers();

        GameManager.Instance.total++;
        GameManager.Instance.AverageCheck();
    }

    public void ForceValue(bool valid, bool fragile, bool sus, bool bomb, string destination)
    {
        if(!valid)
            this.destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];
        else
            this.destination = GameManager.Instance.validDestinationLevel[Random.Range(0, GameManager.Instance.validDestinationLevel.Count)];
        if (destination != null)
            this.destination = destination;

        this.bomb.SetActive(bomb);
        isArmed = bomb;

        isSus = sus;

        isFragile = fragile;

        DestroySticker();
        ApplyStickers();
    }

    void ApplyStickers()
    {
        GameObject sticker = this.gameObject;
        Vector3 stickerPosition = transform.TransformPoint(Random.Range(-0.3f, 0.3f), 0.51f, Random.Range(-0.3f, 0.3f));
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
        {
            if (destination == GameManager.Instance.validDestinationLevel[i])
            {
                sticker = Instantiate(GameManager.Instance.dictionnaryStickers[destination], stickerPosition, Quaternion.identity, transform);
                stickers.Add(sticker);
            }
        }
        for (int i = 0; i < GameManager.Instance.invalidDestinationLevel.Count; i++)
        {
            if (destination == GameManager.Instance.invalidDestinationLevel[i])
            {
                sticker = Instantiate(GameManager.Instance.dictionnaryStickers["Error"], stickerPosition, Quaternion.identity, transform);
                stickers.Add(sticker);
            }
        }
        sticker.transform.Rotate(90f, Random.Range(-180, 180), 0f);
        stickerPosition = transform.TransformPoint(Random.Range(-0.3f, 0.3f), 0.51f, Random.Range(-0.3f, 0.3f));
        if (isFragile)
        {
            sticker = Instantiate(GameManager.Instance.dictionnaryStickers["Fragile"], stickerPosition, Quaternion.identity, transform);
            stickers.Add(sticker);
            sticker.transform.Rotate(90f, Random.Range(-180, 180), 0f);
        }
    }
    void DestroySticker()
    {
        for(int i = 0; i < stickers.Count; i++)
        {
            Destroy(stickers[i]);
        }
        for (int i = 0; i < stickers.Count; i++)
        {
            stickers.RemoveAt(i);
        }
    }

    public void Belt()
    {
        if (isArmed)
        {
            SoundManager.Instance.Play("explosion");
            GameManager.Instance.ChangeLife(GameManager.Instance.lives * -1);
            return;
        }
        bool isValid = false;
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
            if (destination == GameManager.Instance.validDestinationLevel[i]) 
                isValid = true;
        if (isValid)
        {
            SoundManager.Instance.Play("NotValid");
            GameManager.Instance.ChangeLife(-1);
        }
        else
        {
            SoundManager.Instance.Play("NotValid");
            GameManager.Instance.ChangeLife(-1);
        }
        Destroy(gameObject);
    }

    public void Diffuse()
    {
        Bomb bombScript =  bomb.GetComponent<Bomb>();
        for (int i = 0; i < bombScript.buttons.Count; i++)
            bombScript.buttons[i].SetActive(false);
        bomb.SetActive(false);
        isArmed = false;
        SoundManager.Instance.Play("Disarmed");
    }

    public void Crusher()
    {
        if (isArmed) {
            SoundManager.Instance.Play("explosion");
            GameManager.Instance.ChangeLife(GameManager.Instance.lives * -1);
            Destroy(gameObject);
            return;
        }
        if (isBroken) {
            SoundManager.Instance.Play("Valid");
            Destroy(gameObject);
            return;
        }
        if(isSus) {
            SoundManager.Instance.Play("Valid");
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++) {
            if (destination == GameManager.Instance.validDestinationLevel[i]) {
                SoundManager.Instance.Play("NotValid");
                GameManager.Instance.ChangeLife(-1);
                Destroy(gameObject);
                return;
            }
        }
        for (int y = 0; y < GameManager.Instance.invalidDestinationLevel.Count; y++) {
            if (destination == GameManager.Instance.invalidDestinationLevel[y]) {
                SoundManager.Instance.Play("Valid");
                Destroy(gameObject);
                return;
            }
        }
    }

    public void Navette(string shuttleDestination)
    {
        if (isArmed)
        {
            SoundManager.Instance.Play("explosion");
            GameManager.Instance.ChangeLife(GameManager.Instance.lives * -1);
            Destroy(gameObject);
            return;
        }
        if (isBroken)
        {
            SoundManager.Instance.Play("NotValid");
            GameManager.Instance.ChangeLife(-1);
            Destroy(gameObject);
            return;
        }
        if (isSus)
        {
            SoundManager.Instance.Play("NotValid");
            Destroy(gameObject);
            return;
        }
        if (destination == shuttleDestination)
        {
            SoundManager.Instance.Play("Valid");
            Destroy(gameObject);
            return;
        }
        else
        {
            SoundManager.Instance.Play("NotValid");
            GameManager.Instance.ChangeLife(-1);
            Destroy(gameObject);
            return;
        }
    }
}
