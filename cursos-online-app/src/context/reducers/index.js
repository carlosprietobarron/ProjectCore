import { CardActionArea } from "@material-ui/core";
import {userSessionReducer} from "./UserSessionReducer";
import openSnackBarReducer from "./openSnackBarReducer";

export const mainReducer = ({userSessinR, openSnackBar}, action) => {
    return {
        userSessionR: userSessionReducer(userSessinR, action),
        openSnackBar: openSnackBarReducer(openSnackBar, action)
    }
}