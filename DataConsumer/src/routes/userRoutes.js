const userController = require('../controllers/userController');

function setUserRoutes(app, settings) {
    app.get('/user/:code', async (req, res, next) => {await userController.getUser(req, res, next, settings.dataManagerUrl)});
}

module.exports = { setUserRoutes };