using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Order {

    //Called every time this is the active order for the unit
    public abstract void Step(Selectable sel);

    //Used to check and see if the order is finished
    public abstract bool Complete(Selectable sel);

}
