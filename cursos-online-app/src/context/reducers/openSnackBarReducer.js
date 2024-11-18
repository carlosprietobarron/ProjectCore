const initialState = {
    open: false,
    message: ""
};

const openSnackBarReducer = (state = initialState, action) => {
   switch (action.type) {
    case 'open_snackbar':
        return {
            ...state,
            open: action.openMessage.open,
            message: action.openMessage.message
        }
        default:
            return state;
};
}

export default openSnackBarReducer;