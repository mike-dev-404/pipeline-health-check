const axios = require("axios");

let userController = {
  getUser: async (req, res, next, userDomain) => {
    try {
      const code = req.params.code;
      const response = await axios.get(`${userDomain}/user/${code}/`);
      res
        .status(200)
        .json({ username: response.data.username, email: response.data.email });
    } catch (error) {
      res.status(500).json({ error: error.message });
      next(error);
    }
  },
};

module.exports = userController;
