const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {

    if (process.env.REACT_APP_USE_LOCAL) {
        app.use(
            '/api',
            createProxyMiddleware({
                target: process.env.REACT_APP_API_URL,
                xfwd: true,
                secure: false
            })
        );

        app.use(
            '/EAU.Web.Portal.App/signin-oidc',
            createProxyMiddleware({
                target: process.env.REACT_APP_API_URL,
                xfwd: true,
                pathRewrite: {
                    '^/EAU.Web.Portal.App/signin-oidc': '/signin-oidc', // remove base path
                },
                secure: false
            })
        );
    } else {

        app.use(
            '/api',
            createProxyMiddleware({
                target: process.env.REACT_APP_API_URL,
                xfwd: true,
                secure: false
            })
        );

        app.use(
            '/signin-oidc',
            createProxyMiddleware({
                target: process.env.REACT_APP_API_URL,
                xfwd: true,
                secure: false
            })
        );
    }
};