// webpack 빌드 방법은 여기 참고
// https://dev.to/larswillemsens/managing-asp-net-core-mvc-front-end-dependencies-with-npm-and-webpack-part-1-3jf5

const path = require('path');

module.exports = {
    entry: {
        site: './wwwroot/js/site.js',
        datatable : './node_modules/datatables.net/js/jquery.dataTables.min.js'
    },
    output: {
        filename: '[name].min.js',
        path: path.resolve(__dirname, './', 'wwwroot', 'dist')
    },
    devtool: 'source-map',
    mode: 'development',
    module: {
        rules: [
            { test: /\.css$/, use: ['style-loader', 'css-loader'] },
            { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, use: ['file-loader'] },
            {
                test: /\.(woff|woff2)$/, use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 5000,
                        },
                    },
                ]
            },
            {
                test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 10000,
                            mimetype: 'application/octet-stream',
                        },
                    },
                ]
            },
            {
                test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 10000,
                            mimetype: 'image/svg+xml',
                        },
                    },
                ]
            },
        ]
    }
};