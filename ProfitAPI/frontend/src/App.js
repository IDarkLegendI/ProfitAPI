import React, { useState } from 'react';
import ProductList from './components/ProductList';
import ProductForm from './components/ProductForm';

const App = () => {
  const [reload, setReload] = useState(false);

  const handleSave = () => {
    setReload(!reload);
  };

  return (
      <div className="App">
        <h1>Product Management</h1>
        <ProductForm onSave={handleSave} />
        <ProductList key={reload} />
      </div>
  );
};

export default App;
