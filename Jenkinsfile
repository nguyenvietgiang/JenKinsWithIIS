pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/nguyenvietgiang/JenKinsWithIIS.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build Application') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Publish Application') {
            steps {
                // Publish ứng dụng vào thư mục `out`
                bat 'dotnet publish -c Release -o out'
            }
        }

       stage('Deploy to IIS') {
    steps {
        script {
            def iisAppPath = "E:\\Deploy\\JenkinsWithIIS"
            def sourcePath = "${env.WORKSPACE}\\out"
            bat """
            powershell -Command \"
            if (-Not (Test-Path '${iisAppPath}')) {
                New-Item -ItemType Directory -Path '${iisAppPath}'
            }
            Copy-Item -Recurse -Force '${sourcePath}\\*' '${iisAppPath}';
            iisreset;
            \"
            """
        }
    }
}

    }

    post {
        success {
            script {
                def message = "[SUCCESS] Dotnet Web Application build and deploy to IIS successful!"
                bat """
                curl -X POST -H "Content-Type: application/json" -d "{\\"chat_id\\": \\"7094640728\\", \\"text\\": \\"${message}\\", \\"disable_notification\\": false}" https://api.telegram.org/bot7424260362:AAGEoPjAx2uh35FaYIdz-Y0zMVM1so7-wAQ/sendMessage
                """
            }
        }

        failure {
            script {
                def message = "[FAILURE] Dotnet Web Application build and deploy failed!"
                bat """
                curl -X POST -H "Content-Type: application/json" -d "{\\"chat_id\\": \\"7094640728\\", \\"text\\": \\"${message}\\", \\"disable_notification\\": false}" https://api.telegram.org/bot7424260362:AAGEoPjAx2uh35FaYIdz-Y0zMVM1so7-wAQ/sendMessage
                """
            }
        }
    }
}
