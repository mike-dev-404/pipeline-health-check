const axios = require("axios");

let userController = {
  getUser: async (req, res, next, userDomain) => {
    try {
      const code = req.params.code;
      const response = await axios.get(`${userDomain}/user/code/${code}/`);
      res.status(200).json({
        username: response.data.username,
        email: response.data.email,
        nationality: response.data.nationality,
      });
    } catch (error) {
      res.status(500).json({ error: error.message });
      next(error);
    }
  },
  getUsers: async (req, res, next, userDomain) => {
    try {
      const response = await axios.get(`${userDomain}/user/all/`);
      res.status(200).json(
        response.data.map((user) => ({
          username: user.username,
          email: user.email,
          nationality: user.nationality,
        }))
      );
    } catch (error) {
      res.status(500).json({ error: error.message });
      next(error);
    }
  },
  getUsersByNationality: async (req, res, next, userDomain) => {
    try {
      const nationality = req.params.nationality;
      const response = await axios.get(
        `${userDomain}/user/nationality/${nationality}/`
      );
      res.status(200).json(
        response.data.map((user) => ({
          username: user.username,
          email: user.email,
          nationality: user.nationality,
        }))
      );
    } catch (error) {
      res.status(500).json({ error: error.message });
      next(error);
    }
  },
};

module.exports = userController;
