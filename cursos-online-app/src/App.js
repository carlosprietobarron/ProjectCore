import logo from "./logo.svg";
import "./App.css";
import React, { useEffect, useState } from "react";
import { ThemeProvider } from "@material-ui/core/styles"; // Updated import
import theme from "./theme/theme";
import { TextField, Button, Snackbar } from "@material-ui/core";
import UserRegister from "./components/security/UserRegister";
import Login from "./components/security/login";
import UserProfile from "./components/security/userprofile";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; // Updated imports
import { Grid } from "@material-ui/core";
import AppNavBar from "./components/navegation/NavBar";
import { useStateValue } from "./context/store";
import { getCurrentUser } from "./actions/userActions";

function App() {
  // const [{ userSessionR }, dispatch] = useStateValue();
  // const [{ openSnackbar }, dispatchSnackbar] = useStateValue();

  const [{ userSessionR, openSnackbar }, dispatch] = useStateValue();


  const [startApp, setStartApp] = useState(false);

  useEffect(() => {
    if (!startApp) {
      getCurrentUser(dispatch)
        .then((response) => {
          setStartApp(true);
        })
        .catch((error) => setStartApp(true));
    }
  }, [startApp]);

  return (
    <React.Fragment>
      <Snackbar
        anchoOrigin = {{vertical: "bottom", horizontal: "cente"}}
        open={openSnackbar ? openSnackbar.open : false }
        autoHideDuration={3000}
        ContentProps={{"aria-describedby":"message-id"}}
        messare={
          <span id="message-id"> 
            {openSnackbar ? openSnackbar.message: ""}
          </span>
        }
        onClose={ () => {
          dispatch({
            type: "OPEN_SNACKBAR",
            openMessage: {
              open: false,
              message: ""
            }
          })
        }}
      >

      </Snackbar>
      <Router>
        <AppNavBar />
        <ThemeProvider theme={theme}>
          <Grid container>
            <Switch>
              <Route exact path="/auth/login" component={Login} />
              <Route exact path="/auth/register" component={UserRegister} />
              <Route exact path="/auth/profile" component={UserProfile} />
              <Route exact path="/" component={Login} />
            </Switch>
          </Grid>
        </ThemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
