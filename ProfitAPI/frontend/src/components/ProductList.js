import React, { useState, useEffect } from 'react';
import ProductService from '../services/ProductService';

const ProductList = () => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        ProductService.getAllProducts()
            .then(response => setProducts(response.data))
            .catch(error => console.error(error));
    }, []);

    const handleExport = () => {
        ProductService.exportToExcel()
            .then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', 'Products.xlsx');
                document.body.appendChild(link);
                link.click();
                link.remove();
            })
            .catch(error => console.error(error));
    };

    return (
        <div>
            <h2>Product List</h2>
            <button onClick={handleExport}>Export to Excel</button>
            <ul>
                {products.map(product => (
                    <li key={product.id}>
                        {product.name} - ${product.price}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProductList;
