import HttpClient from "../services/HttpClient";
import genericRequest from "../services/HttpClient";

// export const userRegister = (user) =>{
//     return new Promise((resolve,reject) => {
//         HttpClient.post('/user/Signup',user);
//     })
// }

export const userRegister = async (user) => {
    try {
        const response = await HttpClient.post('/user/Signup', user);
        return response;
    } catch (error) {
        throw error;
    }
}



export const getCurrentUser = async (dispatch) => {
    try {
        const response = await HttpClient.get('/user');
        
        // Validar que la respuesta tenga la estructura esperada
        if (!response?.data) {
            throw new Error('Invalid response format');
        }
        
        dispatch({
            type: 'startSession',
            user: response.data,
            authenticated: true
        });
        
        return response;
    } catch (error) {
        // Mejor manejo de errores
        const errorMessage = error.response?.data?.message || error.message || 'Error getting user';
        dispatch({
            type: 'sessionError',
            error: errorMessage
        });
        throw error;
    }
}

export const loginUser = async user => {
    try {
        const response = await HttpClient.post('/user/login', user);
        
        return response;
        
    } catch (error) {
        throw error;
    }
}

// export const updatetUser = async (user) => {
//     try {
//         const response = await HttpClient.put('/user');
//         return response;
//     } catch (error) {
//         return error.response ? error.response : { message: error.message };
//     }
// }

export const updatetUser = async (user) => {
    try {
        const response = await genericRequest.put('/user', user);
        return response;
    } catch (error) {
        return error.response ? error.response : { message: error.message };
    }
};





