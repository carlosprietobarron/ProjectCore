export const initialState = {
    user: {
        FullName: '',
        Email: '',
        UserName: '',
        phtot: ''
    },
    authenticated: false
};

export const userSessionReducer = (state= initialState, action) => {
    switch (action) {
        case "startSession":
            return {
                ...state,
                user: action.session,
                authenticated: action.authenticated
            };
            break;
        case "endSession":
            return {
                ...state,
                user: action.newUser,
                authenticated: action.authenticated
                };
            break;
        case "updateSession":
            return {
                ...state,
                user: action.newUser,
                authenticated: action.authenticated
                 };
            break;    
    }
};