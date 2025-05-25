const userController = require("../controllers/userController");

function setUserRoutes(app, settings) {
  app.get("/user/code/:code", async (req, res, next) => {
    await userController.getUser(req, res, next, settings.dataManagerUrl);
  });
  app.get("/users", async (req, res, next) => {
    await userController.getUsers(req, res, next, settings.dataManagerUrl);
  });
  app.get("/user/nationality/:nationality", async (req, res, next) => {
    await userController.getUsersByNationality(
      req,
      res,
      next,
      settings.dataManagerUrl
    );
  });
}

module.exports = { setUserRoutes };
