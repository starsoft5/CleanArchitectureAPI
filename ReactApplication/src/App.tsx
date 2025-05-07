// src/App.tsx
import React from 'react';
import MyComponent from './components/ProductList';

const App: React.FC = () => {
    return (
        <div>
            <h1>Welcome to the App</h1>
            <MyComponent />
        </div>
    );
};

export default App;
