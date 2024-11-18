import React from 'react';
import style from "../tools/styles.js";
import { Container, Typography, TextField, Button, Grid } from '@material-ui/core';
import { getCurrentUser, updatetUser } from '../../actions/userActions.js';
import { useState, useEffect } from 'react';
import { useStateValue } from "../../context/store.js";

const UserProfile= () => {
    const [user, setUser] = useState({
        FullName: "",
        UserName: "",
        Email:  "",
        Password: "",
        ConfirmPassword: ""
    });

    const setValue = (e) => {
        const { name, value } = e.target;
        setUser(previous => ({
            ...previous,
            [name]: value
        }));
    }

    const [{ userSessionR, openSnackbar }, dispatch] = useStateValue();

    useEffect(() => {
        getCurrentUser().then(response =>{
            console.log("Response from get current user", response); 
            setUser(response.data);
        });
    },[])

    const saveUser = (e => {
        e.preventDefault();
        updatetUser(user).then(response => {
            if(response.status === 200){
                dispatch({
                   type: 'OPEN_SNACKBAR',
                   openmessage:{
                    open: true,
                    message: 'user saved successfully'
                   }
                   
                })
                window.localStorage.setItem("user_token", response.data.token);   
            } else {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    openmessage:{
                     open: true,
                     message: 'user saved successfully'
                    }
                    
                 })
            }

             
        });
    });

    return (
        <Container component='main' maxWidth= "md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant='h5'>
                    User Profile
                </Typography>
                <form style={style.form}>
                    <Grid item xs={12} md={6}>
                        <TextField name="FullName" value={user.FullName} onChange={setValue} variant="outlined" fullWidth placeholder="full Name"  margin="normal" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="UerName" value={user.UerName} onChange={setValue} variant="outlined" fullWidth placeholder="User Name"  margin="normal" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Email" value={user.Email} onChange={setValue} variant="outlined" fullWidth placeholder="Email"  margin="normal" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Password" type="password" value={user.Password} onChange={setValue} variant="outlined" fullWidth placeholder="password" required  margin="normal"></TextField>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="ConfirmPassword" value={user.ConfirmPassword} onChange={setValue} type="password" variant="outlined" fullWidth placeholder="Confirm password" required  margin="normal"></TextField>
                    </Grid>
                    < Grid item xs={12} md={6} justify="center">
                        <Button type="submit" onClick={saveUser} fullWidth variant="contained" color="primary" size="medium" style={style.submit}>
                            SAVE
                        </Button>
                    </Grid>
                </form>
            </div>
        </Container>
    )
}

export default UserProfile
