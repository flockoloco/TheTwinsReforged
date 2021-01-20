package com.example.mainactivity.retrofit

import io.reactivex.Observable
import io.reactivex.ObservableEmitter
import retrofit2.http.Field
import retrofit2.http.FormUrlEncoded
import retrofit2.http.POST

interface INodeJS {
    @POST("register")
    @FormUrlEncoded
    fun registerUser(
        @Field("Username") Username: String,
        @Field("Password") Password: String
    ): Observable<String>


    @POST("login")
    @FormUrlEncoded
    fun loginUser(
        @Field("Username") Username: String,
        @Field("Password") Password: String
    ): Observable<String>

    @POST("user")
    @FormUrlEncoded
    fun getApp(@Field("UserID") UserID: Int): Observable<String>

    @POST("sendDB")
    @FormUrlEncoded
    fun sendDB(
        @Field("Gold") Gold: Int,
        @Field("Nuggets") Nuggets: Int,
        @Field("Bars") Bars: Int,
        @Field("Minespd") Minespd: Int,
        @Field("MineHarvest") MineHarvest: Int,
        @Field("PermUpgrade") PermUpgrade: Int,
        @Field("FirstTime") FirstTime: Int,
        @Field("UserID") UserID: Int
    ): Observable<String>

    @POST("sendDelivery")
    @FormUrlEncoded
    fun sendDelivery(
        @Field("UserID") UserID: Int,
        @Field("BarsAmount") BarsAmount: Int,
        @Field("OresAmount") OresAmount: Int,
        @Field("Type") Type: Int
    ): Observable<String>

    @POST("acceptDelivery")
    @FormUrlEncoded
    fun receiveDelivery(
        @Field("UserID") UserID: Int,
        @Field("Type") Type: Int
    ): Observable<String>

    @POST("checkDelivery")
    @FormUrlEncoded
    fun checkDelivery(
        @Field("UserID") UserID: Int,
        @Field("Type") Type: Int
    ): Observable<String>
}