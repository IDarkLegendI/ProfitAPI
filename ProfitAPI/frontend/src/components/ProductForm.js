import React, { useState } from 'react';
import ProductService from '../services/ProductService';

const ProductForm = ({ product, onSave }) => {
    const [formData, setFormData] = useState(
        product || { name: '', description: '', price: 0, stock: 0 }
    );

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (formData.id) {
            ProductService.updateProduct(formData.id, formData)
                .then(() => onSave())
                .catch(console.error);
        } else {
            ProductService.createProduct(formData)
                .then(() => onSave())
                .catch(console.error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                name="name"
                placeholder="Name"
                value={formData.name}
                onChange={handleChange}
            />
            <input
                name="description"
                placeholder="Description"
                value={formData.description}
                onChange={handleChange}
            />
            <input
                type="number"
                name="price"
                placeholder="Price"
                value={formData.price}
                onChange={handleChange}
            />
            <input
                type="number"
                name="stock"
                placeholder="Stock"
                value={formData.stock}
                onChange={handleChange}
            />
            <button type="submit">Save</button>
        </form>
    );
};

export default ProductForm;
