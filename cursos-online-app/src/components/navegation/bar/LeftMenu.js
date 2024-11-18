import React from "react"
import {
    Avatar,
    Button,
    Drawer,
    IconButton,
    Toolbar,
    Typography,
    makeStyles,
    List,
    ListItemText,
    ListItem,
    Link,
    Divider
  } from "@material-ui/core";


export const LeftMenu = (props) => {
    const {classes} = props;
    const {closeLeftMenu} = props;

<div className={classes.lits} onKeyDown={closeLeftMenu} onClick={closeLeftMenu}>
    <List>
        <ListItem component={Link} button to="/auth/profile">
            <i className="material-icons>">account box</i>
            <ListItemText classes={{primary: classes.ListItemText}} primary="Perfil" /> 
        </ListItem>
        <ListItem component={Link} button to="/auth/login">
            <i className="material-icons>">account box</i>
            <ListItemText classes={{primary: classes.ListItemText}} primary="login" /> 
        </ListItem>
    </List>
    <Divider/>
    <List>
        <ListItem component={Link} button to="/auth/login">
            <i className="material-icons>">account box</i>
            <ListItemText classes={{primary: classes.ListItemText}} primary="login" /> 
        </ListItem>
        
    </List>
</div>

}
