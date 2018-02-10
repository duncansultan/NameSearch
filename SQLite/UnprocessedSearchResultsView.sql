CREATE VIEW UnprocessedSearchResultsView
AS
SELECT SR.Id AS PersonSearchResultId, 
       RQ.Id AS PersonSearchRequestId, 
       RQ.Name,
	   RQ.Address1, 
       RQ.Address2, 
       RQ.City, 
       RQ.State, 
       RQ.Zip 
FROM   PersonSearchRequests RQ 
       INNER JOIN PersonSearchResults SR 
               ON RQ.Id = SR.PersonSearchRequestId 
WHERE  SR.Id NOT IN (SELECT PersonSearchResultId 
                     FROM   People);