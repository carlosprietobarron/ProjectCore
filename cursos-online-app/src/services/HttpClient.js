import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use(
    (config) => {
        const user_token = window.localStorage.getItem('user_token');
        if (user_token) {
            config.headers.Authorization = `Bearer ${user_token}`;
            return config;
        }
        return Promise.reject('No token found');
    },
    (error) => {
        // Manejo del error en la interceptación
        return Promise.reject({
            message: 'Error in request interceptor',
            error: error
        });
    }
);

// También puedes agregar un interceptor de respuesta para manejar errores de token inválido
axios.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response?.status === 401) {
            // Token expirado o inválido
            window.localStorage.removeItem('user_token'); // Eliminar el token
            // Redirigir al login u otra acción
            window.location.href = '/login';
            return Promise.reject({
                message: 'Invalid or expired token',
                error: error
            });
        }
        return Promise.reject(error);
    }
);

const genericRequest = {
    get: (url) => axios.get(url).then(response => response),
    post: (url, body) => axios.post(url, body).then(response => response),
    put: (url, body) => axios.put(url, body).then(response => response),
    delete: (url) => axios.delete(url).then(response => response)
}

export default genericRequest;