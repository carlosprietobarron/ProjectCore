import React, { useState } from "react";
import { Container, Grid, TextField, Typography, Button } from "@material-ui/core";
import style from "../tools/styles.js";
import { userRegister } from "../../actions/userActions.js";


// const style = {
//     paper: {
//         marginTop: 8,
//         display: "flex",
//         flexDirection: "column",
//         alignItems: "center",
//     },
//     form: {
//         width: "100%",
//         marginTop: 20
//     },
//     submit: {
//         marginTop: 15
//     }
// }

const UserRegister = () => {
    const [user, setUser] = useState({
        FullName: "",
        UserName: "",
        Email:  "",
        Password: "",
        ConfirmPassword: ""
    })

    const setValue = (e) => {
        const { name, value } = e.target;
        setUser(previous => ({
            ...previous,
            [name]: value
        }));
    }

    const sendCommand = (e) => {
        e.preventDefault();
        console.log("send values: " + user);
        userRegister(user).then((response) => {
            console.log("User Registered successfully. ", response);
            window.localStorage.setItem("user_token", response.data.token);
        })
    }



   
    
    return(
        <Container component='main' maxWidth='md' justify='center' alignItems="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">User Register</Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField name="FullName" value={user.FullName} onChange={setValue} variant="outlined" fullWidth placeholder="User Name" required></TextField>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Email" value={user.Email} onChange={setValue} variant="standard" fullWidth placeholder="Email" required></TextField>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="UserName" value={user.UserName} onChange={setValue} variant="filled" fullWidth placeholder="user name" required></TextField>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Password" value={user.Password} onChange={setValue} type="password" variant="outlined" fullWidth placeholder="password" required></TextField>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="ConfirmPassword" value={user.ConfirmPassword} onChange={setValue} type="password" variant="outlined" fullWidth placeholder="confirm password" required></TextField>
                        </Grid>
                        < Grid item xs={12} md={6} justify="center">
                            <Button type="submit" fullWidth variant="contained" color="primary" size="medium" onClick={sendCommand}>
                                Enviar
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default UserRegister;