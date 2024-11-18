import React, { useState} from 'react';
import style from "../tools/styles.js";
import { Avatar, Container, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { loginUser } from '../../actions/userActions.js';

const Login = () => {
    const [user, setUser] = useState({
        Email:  "",
        Password: "",
    });

    const setValue = (e) => {
        const { name, value } = e.target;
        setUser(previous => ({
            ...previous,
            [name]: value
        }));
    }

    const loginUserBtn = (e) => {  
        e.preventDefault();
        loginUser(user).then (response =>{
            console.log("successfully logged in");
            window.localStorage.setItem("user_token", response.data.token);
        })
    }

    return (
        <Container maxWidth="xs">
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.icon}/>
                </Avatar>
                <Typography component="h1" variant="h5">
                    Login de usuario
                </Typography>
                <form style={style.form}>
                    <TextField  name="Email" value={user.Email} onChange={setValue} variant="outlined" fullWidth placeholder="User Name" margin="normal"></TextField>
                    <TextField name="password" type='password' value={user.Password} onChange={setValue}  variant="outlined" fullWidth placeholder="password" margin="normal">></TextField>
                    <Button type="submit" onClick={loginUserBtn} fullWidth variant="contained" color="primary" size="medium" style={style.submit}>
                        Login
                    </Button>
                </form>   
            </div>
        </Container>
    )
}

export default  Login