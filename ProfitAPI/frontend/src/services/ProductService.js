import axios from 'axios';

const API_URL = "http://localhost:5200/api/products";

const getAllProducts = () => axios.get(API_URL);
const getProductById = (id) => axios.get(`${API_URL}/${id}`); 
export const createProduct = async (productData) => {
    try {
        const response = await axios.post(API_URL, productData);
        return response.data;
    } catch (error) {
        console.error("Ошибка при создании продукта:", error);
        throw error;
    }
};
const updateProduct = (id, product) => axios.put(`${API_URL}/${id}`, product); 
const deleteProduct = (id) => axios.delete(`${API_URL}/${id}`); 
const exportToExcel = () => axios.get(`${API_URL}/export`, { responseType: 'blob' });

export default {
    getAllProducts,
    getProductById,
    createProduct,
    updateProduct,
    deleteProduct,
    exportToExcel
};
