const { defineConfig } = require('@vue/cli-service');

module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:8081',
      }
    },
  },
  configureWebpack: (config) => {
    const newRule = {
      test: /\.(png|jpe?g|gif|webp|avif)(\?.*)?$/,
        type: 'asset',
        generator: {
          filename: 'img/[name].[hash:16][ext]'
        },
    };
    const imagesRuleIndex = config.module.rules.findIndex(x => x.test.source.includes('png|jpe?g|'));
    config.module.rules.splice(imagesRuleIndex, 1, newRule);
    config.module.rules.push({
      test: /\.coffee$/,
      use: ['coffee-loader'],
    },
    )
  }
});
