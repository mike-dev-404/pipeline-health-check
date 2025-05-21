const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');
const path = require('path');

const { setUserRoutes } = require('./routes/userRoutes');

const settingsPath = path.join(__dirname, './config/settings.json');
const settings = JSON.parse(fs.readFileSync(settingsPath, 'utf-8'));

const app = express();
const PORT = settings.port || 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

setUserRoutes(app, settings);

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});