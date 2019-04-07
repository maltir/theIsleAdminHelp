package com.firePigeon.theIsleAdminHelpTool;

import java.util.Locale;
import java.util.ResourceBundle;

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;
import javafx.fxml.FXMLLoader;

public class App extends Application
{
	@Override
    public void start(Stage primaryStage) throws Exception {
		ResourceBundle languageResource = ResourceBundle.getBundle("i18n.text", Locale.getDefault());
		Pane root = (Pane) FXMLLoader.load(App.class.getResource("fxml/start.fxml"),languageResource);

        primaryStage.setScene(new Scene(root));
        primaryStage.setTitle("the Isle Admin Help Tool");
        primaryStage.show();
    }
	
	public static void main(String[] args) {
        launch();
    }
}
