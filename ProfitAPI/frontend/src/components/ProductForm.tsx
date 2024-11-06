import React, { useState } from 'react';
import ProductService, { Product } from '../services/ProductService';

interface ProductFormProps {
    product?: Product;
    onSave: () => void;
}

const ProductForm: React.FC<ProductFormProps> = ({ product, onSave }) => {
    const [formData, setFormData] = useState<Product>(
        product || { name: '', description: '', price: 0, stock: 0 }
    );

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: name === 'price' || name === 'stock' ? +value : value });
    };

    const handleSubmit = (e: React.FormEvent) => {
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
            <input name="name" placeholder="Name" value={formData.name} onChange={handleChange} />
            <input name="description" placeholder="Description" value={formData.description} onChange={handleChange} />
            <input type="number" name="price" placeholder="Price" value={formData.price} onChange={handleChange} />
            <input type="number" name="stock" placeholder="Stock" value={formData.stock} onChange={handleChange} />
            <button type="submit">Save</button>
        </form>
    );
};

export default ProductForm;
