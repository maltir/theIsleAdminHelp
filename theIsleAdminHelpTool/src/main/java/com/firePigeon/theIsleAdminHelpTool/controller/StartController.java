package com.firePigeon.theIsleAdminHelpTool.controller;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.MenuItem;

public class StartController{

    @FXML
    private Button btn_ban;

    @FXML
    private Button btn_stat;
    
    @FXML
    private MenuItem menuItem_close;

    @FXML
    private MenuItem menuItem_About;

    @FXML
    void onBanClick(ActionEvent event) {
    	System.out.println("Ban click");
    }

    @FXML
    void onStatClick(ActionEvent event) {
    	System.out.println("Stat click");
    }
    
    @FXML
    void onAboutClick(ActionEvent event) {
    	System.out.println("About click");
    }
    
    @FXML
    void onCloseClick(ActionEvent event) {
    	System.out.println("Close click");
    }

}