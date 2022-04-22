# Kafka Producer C# コンソールアプリケーション

## 概要
　Kafka Producer 用の C# コンソールアプリケーションです。[Azure Functions の Kafka Trigger を利用した C# サンプル](https://github.com/Yuichi-Ikeda/KafkaFunctionApp)の負荷ツールとして作成しています。

[ベースサンプル](https://developer.confluent.io/get-started/dotnet#build-producer)を元に最新版の Visual Studio 2022 にて新規作成しています。

### ローカル開発環境
- Visual Studio 2022

ローカル実行には Kafka.config ファイルを用意してプロジェクトの引数で config ファイルへのパスを渡す必要があります。

```Kafka.config
bootstrap.servers=<Your Domain>.confluent.cloud:9092
security.protocol=SASL_SSL
sasl.mechanisms=PLAIN
sasl.username=<Your API Key>
sasl.password=<Your API Secret>
```

KafkaProducer.exe <configuration file path> [numMessages]
