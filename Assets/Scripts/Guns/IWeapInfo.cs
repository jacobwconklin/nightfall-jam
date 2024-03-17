using System.Collections;
using UnityEngine;

public interface IWeapInfo
{
    void Reload();

    string GetWeapType();

    int GetAmmo();

    int GetMag();

    void disableShooting();

    void enableShooting();
}