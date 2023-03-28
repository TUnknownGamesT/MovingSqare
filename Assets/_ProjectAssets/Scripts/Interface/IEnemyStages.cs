using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stages
{
   first,
   second,
}

public interface IEnemyStages
{
   public void Stage();
}
