import React, {useState } from "react";
import { LeftMenu } from './LeftMenu.js';
import { RightMenu } from "./RightMenu.js";

import {
  Avatar,
  Button,
  Drawer,
  IconButton,
  Toolbar,
  Typography,
  makeStyles} from "@material-ui/core";


import userPhoto from "../../../logo.svg";
import { useStateValue } from "../../../context/store";
import { withRouter } from "react-router-dom/cjs/react-router-dom.min";

const useStyles = makeStyles((theme) => ({
  DesktopSctn: {
    display: "none",
    [theme.breakpoints.up("md")]: {
      display: "flex",
    },
  },
  MobileSctn: {
    display: "flex",
    [theme.breakpoints.up("md")]: {
      display: "none",
    },
  },
  grow: {
    flexGrow: 1,
  },
  avatarSize: {
    height: 40,
    width: 40,
  },
  list: {
    width: 250,
  },
  ListItem: {
    fontSize: "14",
    fontWeight: 600,
    paddingLeft: "15",
    color: "#212121",
  }
}));

const BarSesion = (props) => {
  const classes = useStyles();
  const [{ userSessionR }, dispatch] = useStateValue();
  const [leftOpen, setLeftOpen] = useState(false);
  const [rightOpen, setRightOpen] = useState(false);

  const closeLeftMenu = () =>{
    setLeftOpen(false);
  };

  const closeRightMenu = () =>{
    setRightOpen(false);
  };

  const openLeftMenu = () =>{
    setLeftOpen(true);
  }

  const openRightMenu = () =>{
    setRightOpen(true);
  }

  const closeSession = () =>{
    localStorage.removeItem("user_token");
    props.history.push('auth/')
  }

  return (
    <>
      <Drawer 
        open={leftOpen}
        onClose={closeLeftMenu}
        anchor="left"
        >
            <div className={classes.list} onKeyDown={closeLeftMenu} onClick={closeLeftMenu}>
                <LeftMenu classes={classes}/>
            </div>
      </Drawer>
      <Drawer 
        open={rightOpen}
        onClose={closeRightMenu}
        anchor="right"
        >
            <div  role="button" onKeyDown={closeRightMenu} onClick={closeRightMenu}>
                <rightMenu 
                    classes={classes} 
                    closeSession={closeSession}
                    user ={userSessionR.user}
                    />
            </div>
      </Drawer>
      <Toolbar>
        <IconButton color="inherit" onClick={openLeftMenu}>
          <i className="material-icons">menu</i>
        </IconButton>
        <Typography variant="h6">RamaDeneb's Online Courses</Typography>
        <div className={classes.grow}></div>

        <div className={classes.DesktopSctn}>
          <Button color="inherit">Exit</Button>
          <Button color="inherit">
            {userSessionR ? userSessionR.user.FullName : ""}
          </Button>
          <Avatar src={userPhoto}></Avatar>
        </div>

        <div className={classes.MobileSctn}>
          <IconButton color="inherit">
            <i className="material-icons">more_vert</i>
          </IconButton>
        </div>
      </Toolbar>
    </>
  );
};

export default withRouter (BarSesion);
