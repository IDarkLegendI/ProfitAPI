import axios, { AxiosResponse } from 'axios';

const API_URL = 'http://localhost:5200/api/products';

export interface Product {
    id?: number;
    name: string;
    description: string;
    price: number;
    stock: number;
}

const getAllProducts = (): Promise<AxiosResponse<Product[]>> => axios.get(API_URL);
const getProductById = (id: number): Promise<AxiosResponse<Product>> => axios.get(`${API_URL}/${id}`);

export const createProduct = async (productData: Product): Promise<Product> => {
    try {
        const response = await axios.post<Product>(API_URL, productData);
        return response.data;
    } catch (error) {
        console.error('Ошибка при создании продукта:', error);
        throw error;
    }
};

const updateProduct = (id: number, product: Product): Promise<AxiosResponse<Product>> =>
    axios.put(`${API_URL}/${id}`, product);

const deleteProduct = (id: number): Promise<AxiosResponse<void>> => axios.delete(`${API_URL}/${id}`);

const exportToExcel = (): Promise<AxiosResponse<Blob>> =>
    axios.get(`${API_URL}/export`, { responseType: 'blob' });

export default {
    getAllProducts,
    getProductById,
    createProduct,
    updateProduct,
    deleteProduct,
    exportToExcel,
};
