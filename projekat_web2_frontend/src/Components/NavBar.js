import React from "react";
import { Link, NavLink } from "react-router-dom";

const NavBar = () => {

    const active = (isActive) =>{
        if(isActive)
            return "item active"
        else
            return "item"
    }

    return (
        <div className="ui blue secondary pointing menu">
            <NavLink className={({isActive}) => active(isActive)} to="/">Home page</NavLink>
            <NavLink className={({isActive}) => active(isActive)} to="/login">Log in</NavLink>
            <NavLink className={({isActive}) => active(isActive)} to="/registration">Registration</NavLink>
        </div>
    )
}


export default NavBar;