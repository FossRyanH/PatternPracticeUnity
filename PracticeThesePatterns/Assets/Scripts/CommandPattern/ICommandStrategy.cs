using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandStrategy
{
    Command CreateCommand(Vector3 position);
}
