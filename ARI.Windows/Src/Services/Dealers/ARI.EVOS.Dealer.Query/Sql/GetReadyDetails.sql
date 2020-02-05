select country_code as CountryCode,
       make_code as MakeCode,
	   dealer_id as DealerId,
       get_ready_category as GetReadyCategory, 
       client_id as ClientId,
	   get_ready_amt as GetReadyAmount,
	   get_ready_eff_date as GetReadyEffectiveDate,
	   last_prgm as LastProgram,
	   last_user as LastUser,
	   last_chg as LastChange 
from GET_READY 
where country_code=:CountryCode 
     and make_code=:MakeCode 
	 and dealer_id=:DealerId