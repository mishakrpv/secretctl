package config

import (
	"encoding/json"
	"io"
	"os"

	"github.com/confluentinc/confluent-kafka-go/v2/kafka"
)

type ProxyConfig struct {
	Routes []route `json:"Routes"`
}

type route struct {
	DownstreamTopicPartition kafka.TopicPartition   `json:"DownstreamTopicPartition"`
	DownstreamMessage        map[string]interface{} `json:"DownstreamMessage"`
	UpstreamPathTemplate     string                 `json:"UpstreamPathTemplate"`
	UpstreamHttpMethod       string                 `json:"UpstreamHttpMethod"`
}

func GetProxyConfig(path string) (config *ProxyConfig) {
	file, err := os.Open(path)
	if err != nil {
		panic(err)
	}

	defer file.Close()

	butes, err := io.ReadAll(file)
	if err != nil {
		panic(err)
	}

	json.Unmarshal(butes, &config)

	return
}
