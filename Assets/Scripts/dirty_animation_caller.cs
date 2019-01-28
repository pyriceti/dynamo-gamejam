using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirty_animation_caller : MonoBehaviour {

    public player_shoot ps;
    public player_controller pc; 


    public void dirty_throw_end_caller()
    {
        ps.ball_throw();
    }

}
