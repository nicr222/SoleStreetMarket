const mysql = require('mysql2');
const express = require('express');
const bodyParser = require('body-parser');

const app = express();
const port = 3000;

/*const sourceDb = mysql.createConnection({
    // ... source database configuration
});
dont need this 
const targetDb = mysql.createConnection({
    // ... target database configuration
});*/

app.use(bodyParser.json());

// Handle product additions to the cart
app.post('/addToCart', (req, res) => {
    const productId = req.body.productId;
    const quantity = req.body.quantity;

    // Fetch product details from the source database
    sourceDb.query('SELECT * FROM products WHERE id = ?', [productId], (err, rows) => {
        if (err || rows.length === 0) {
            return res.status(404).json({ error: 'Product not found' });
        }

        const product = rows[0];

        // Insert the selected product into the cart table in the target database
        targetDb.query('INSERT INTO cart (product_id, description, quantity) VALUES (?, ?, ?)',
            [product.id, product.description, quantity], (err) => {
                if (err) {
                    return res.status(500).json({ error: 'Error adding product to cart' });
                }

                return res.status(200).json({ message: 'Product added to cart successfully' });
            });
    });
});

app.listen(port, () => {
    console.log(`Server is listening on port ${port}`);
});