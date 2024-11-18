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

import userPhoto from "../../../logo.svg";

export const RightMenu = (props) => {
    const {classes} = props;
    const {closeSessions} = props;
    const {user} = props;
    
<div className={classes.lits} >
    <List>
        <ListItem button component={Link}>
            <Avatar scr={user.photo || userPhoto}/>
            <ListItemText classes={{primary: classes.ListItemText}} primary={user ? user.FullName : ""} />
        </ListItem>
        <ListItem component={Link} button to="/auth/login">
            <ListItemText classes={{primary: classes.ListItemText}} primary="LogOut" />  
        </ListItem>
    </List>
</div>

}
